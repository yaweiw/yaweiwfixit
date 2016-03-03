using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading;
using System.Web.Script.Serialization;
using System.Runtime;

namespace MessageComposer
{
    /// <summary>
    /// Represents a message
    /// </summary>
    public class Message : AmbientContextQueueTransport
    {
        /// <summary>
        /// Gets or sets the identifier of the message
        /// </summary>
        public MessageId MessageId { get; set; }

        /// <summary>
        /// Gets or sets the message priority
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }

        /// <summary>
        /// Gets or sets who triggered the message
        /// </summary>
        public string TriggeredBy { get; set; }

        #region SubMessage

        /// <summary>
        /// Gets or sets the category of the message, e.g. TOC or article
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the index of sub message
        /// </summary>
        public long SubMessageIndex { get; set; }

        #endregion

        public override string ToString()
        {
            return $"MessageId: {MessageId}, Category: {Category}, Priority: {Priority}, SubMessageIndex: {SubMessageIndex}";
        }
    }

    /// <summary>
    /// Inherit AmbientContext from Queue Message
    /// CorrelationId, AmbientContextFlags and AmbientContextDownstream will be inherited to backend worker and passed to backend web service call, e.g. document hosting service
    /// Feedback will not be inherited to backend worker
    /// </summary>
    public class AmbientContextQueueTransport
    {
        private const string AmbientContextIdKey = "AmbientContextId";
        private const string AmbientContextFlagsKey = "AmbientContextFlags";
        private const string AmbientContextDownstreamKey = "AmbientContextDownstream";

        private static readonly ThreadLocal<JavaScriptSerializer> Serializer = new ThreadLocal<JavaScriptSerializer>(() => new JavaScriptSerializer());

        public string CorrelationId { get; set; }

        public AmbientContextFlags AmbientContextFlags { get; set; }

        public IDictionary<string, object> AmbientContextDownstream { get; set; }

        public AmbientContextQueueTransport()
        {
            // set AmbientContextFlags to AMBCTX_DOWNSTREAM_PROPAGATION, which means ambient context will be passed across web service call
            AmbientContextFlags = AmbientContextFlags.AMBCTX_DOWNSTREAM_PROPAGATION;
        }

        public void BranchFromCurrentContext()
        {
            var context = GetCurrentAmbientContext();
            var branch = context.CreateBranch();

            CorrelationId = branch.Id;

            // Inherit flags from context
            AmbientContextFlags = context.Flags;
            AmbientContextDownstream = context.DownstreamContext;
        }

        public AmbientContext GetCurrentAmbientContext()
        {
            var context = AmbientContext.TryGetCurrentContext();
            if (context == null)
            {
                if (string.IsNullOrWhiteSpace(CorrelationId))
                {
                    CorrelationId = Guid.NewGuid().ToString();
                }

                var upstreamHeaders = new Dictionary<string, object>
                {
                    { AmbientContextIdKey, CorrelationId },
                    { AmbientContextFlagsKey, AmbientContextFlags },
                };

                // TODO: do not pass on AmbientContextDownstream if it is too large for Azure queue
                if (AmbientContextDownstream != null)
                {
                    upstreamHeaders.Add(AmbientContextDownstreamKey, AmbientContextDownstream);
                }

                context = AmbientContext.InitializeAmbientContext(Serializer.Value.Serialize(upstreamHeaders));
            }

            return context;
        }
    }
}
