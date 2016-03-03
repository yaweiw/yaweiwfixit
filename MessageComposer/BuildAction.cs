namespace MessageComposer
{
    /// <summary>
    /// Actions in build service
    /// </summary>
    public enum BuildAction
    {
        /// <summary>
        /// Build the contents and publish to Document Hosting Service
        /// </summary>
        Publish,

        /// <summary>
        /// Build the contents only
        /// </summary>
        Build
    }
}
