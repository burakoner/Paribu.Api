namespace Paribu.Api.Helpers;

public static class ParibuExtensions
{

    #region Null
    public static bool IsNull(this object @this)
    {
        return (@this == null || @this.GetType() == typeof(DBNull));
    }

    public static bool IsNotNull(this object @this)
    {
        return !@this.IsNull();
    }
    #endregion

    #region ToStr
    public static string ToStr(this object @this, bool nullToEmpty = true)
    {
        bool isNull = @this == null ? true : false;
        bool isDBNull = @this != null && @this.GetType() == typeof(DBNull) ? true : false;

        if (isNull)
            return nullToEmpty ? string.Empty : null;
        else if (isDBNull)
            return nullToEmpty ? string.Empty : null;
        else
            return @this?.ToString();
    }
    #endregion

    #region ToNumber
    public static int ToInt(this object @this)
    {
        int result = 0;
        if (@this.IsNotNull()) int.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static long ToLong(this object @this)
    {
        long result = 0;
        if (@this.IsNotNull()) long.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static double ToDouble(this object @this)
    {
        if (@this == null) return 0.0;

        double result = 0.0;
        if (@this.IsNotNull()) double.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    public static double? ToDoubleNullable(this object @this)
    {
        if (@this == null) return null;

        double result = 0.0;
        if (@this.IsNotNull()) double.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static decimal ToDecimal(this object @this)
    {
        if (@this == null) return 0;

        decimal result = 0.0m;
        if (@this.IsNotNull()) decimal.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    public static decimal? ToDecimalNullable(this object @this)
    {
        if (@this == null) return null;

        decimal result = 0.0m;
        if (@this.IsNotNull()) decimal.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static float ToFloat(this object @this)
    {
        if (@this == null) return 0;

        float result = 0;
        if (@this.IsNotNull()) float.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    #endregion

}
