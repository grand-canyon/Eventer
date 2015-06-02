namespace Eventer.Models
{
    using System.ComponentModel;

    public enum EventStatus
    {
        [Description("Open")]
        Open,

        [Description("Closed")]
        Closed,

        [Description("Private")]
        Private
    }
}