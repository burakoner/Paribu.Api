namespace Paribu.Api.Enums;

public enum ParibuAssetType : byte
{
    [Map("fiat")]
    Fiat = 1,

    [Map("crypto")]
    Crypto = 2,
}