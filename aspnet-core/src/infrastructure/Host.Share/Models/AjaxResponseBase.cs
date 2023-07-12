namespace Host.Share.Models
{
    public abstract class AjaxResponseBase
    {

        public string TargetUrl { get; set; }

        /// <summary>
        /// Indicates success status of the result.
        /// Set <see cref="Error"/> if this value is false.
        /// </summary>
        public bool Success { get; set; }


    }
}