namespace MessageComposer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Represents the identifier of the message
    /// </summary>
    public class MessageId
    {
        /// <summary>
        /// Gets or sets the repository id
        /// </summary>
        public RepoId RepoId { get; set; }

        /// <summary>
        /// Gets or sets the build id
        /// </summary>
        public BuildId BuildId { get; set; }

        /// <summary>
        /// Gets or sets the build type, e.g. Commit or PullRequest
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public BuildType BuildType { get; set; }

        /// <summary>
        /// Gets or sets the build action, e.g. Build or Publish
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public BuildAction Action { get; set; }

        public override string ToString()
        {
            return $"RepoId: {this.RepoId}, BuildId: {this.BuildId}, BuildType: {this.BuildType}, Action: {this.Action}";
        }
    }
}
