
namespace AeroGear
{
    public enum MessageType
    {
        badge,
        raw,
        tile,
        toast
    };

    public enum BadgeType
    {
        none,
        activity,
        alert,
        available,
        away,
        busy,
        newMessage,
        paused,
        playing,
        unavailable,
        error,
        attention
    }

    public enum ToastType
    {
        ToastText01,
        ToastText02,
        ToastText03,
        ToastText04,

        ToastImageAndText01,
        ToastImageAndText02,
        ToastImageAndText03,
        ToastImageAndText04
    }

    public enum TileType
    {
        TileSquareBlock,
        TileSquareText01,
        TileSquareText02,
        TileSquareText03,
        TileSquareText04,

        TileSquareImage,

        TileSquarePeekImageAndText01,
        TileSquarePeekImageAndText02,
        TileSquarePeekImageAndText03,
        TileSquarePeekImageAndText04,

        TileWideText01,
        TileWideText02,
        TileWideText03,
        TileWideText04,
        TileWideText05,
        TileWideText06,
        TileWideText07,
        TileWideText08,
        TileWideText09,
        TileWideText10,
        TileWideText11,

        TileWideImage,
        TileWideImageCollection,

        TileWideImageAndText01,
        TileWideImageAndText02,
        TileWideBlockAndText01,
        TileWideBlockAndText02,
        TileWideSmallImageAndText01,
        TileWideSmallImageAndText02,
        TileWideSmallImageAndText03,
        TileWideSmallImageAndText04,
        TileWideSmallImageAndText05,

        TileWidePeekImageCollection01,
        TileWidePeekImageCollection02,
        TileWidePeekImageCollection03,
        TileWidePeekImageCollection04,
        TileWidePeekImageCollection05,
        TileWidePeekImageCollection06,
        TileWidePeekImageAndText01,
        TileWidePeekImageAndText02,
        TileWidePeekImage01,
        TileWidePeekImage02,
        TileWidePeekImage03,
        TileWidePeekImage04,
        TileWidePeekImage05,
        TileWidePeekImage06
    }
}
