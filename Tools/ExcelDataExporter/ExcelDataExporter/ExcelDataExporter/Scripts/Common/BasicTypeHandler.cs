using System;
using System.Collections.Generic;

public delegate bool TryParseDelegate<T>(string str, out T value);
public delegate T BinocularOperationDelegate<T>(T a, T b);
public static class BasicTypeMethodUtility
{
    private class IMethodHodler { }

    private class MethodHolder<T> : IMethodHodler
    {
        public BinocularOperationDelegate<T> Add;
        public BinocularOperationDelegate<T> Minus;
        public BinocularOperationDelegate<T> Multiply;
        public BinocularOperationDelegate<T> Devide;
        public TryParseDelegate<T> TryParse;
    }

    private static Dictionary<Type, IMethodHodler> m_methodHolderMap = new Dictionary<Type, IMethodHodler>();

    static BasicTypeMethodUtility()
    {
        MethodHolder<byte> byteMethodHolder = new MethodHolder<byte>();
        byteMethodHolder.Add = Add;
        byteMethodHolder.Minus = Minus;
        byteMethodHolder.Multiply = Multiply;
        byteMethodHolder.Devide = Devide;
        byteMethodHolder.TryParse = TryParse;
        m_methodHolderMap.Add(typeof (byte), byteMethodHolder);

        MethodHolder<short> shortMethodHolder = new MethodHolder<short>();
        shortMethodHolder.Add = Add;
        shortMethodHolder.Minus = Minus;
        shortMethodHolder.Multiply = Multiply;
        shortMethodHolder.Devide = Devide;
        shortMethodHolder.TryParse = TryParse;
        m_methodHolderMap.Add(typeof(short), shortMethodHolder);

        MethodHolder<int> intMethodHolder = new MethodHolder<int>();
        intMethodHolder.Add = Add;
        intMethodHolder.Minus = Minus;
        intMethodHolder.Multiply = Multiply;
        intMethodHolder.Devide = Devide;
        intMethodHolder.TryParse = TryParse;
        m_methodHolderMap.Add(typeof(int), intMethodHolder);

        MethodHolder<float> floatMethodHolder = new MethodHolder<float>();
        floatMethodHolder.Add = Add;
        floatMethodHolder.Minus = Minus;
        floatMethodHolder.Multiply = Multiply;
        floatMethodHolder.Devide = Devide;
        floatMethodHolder.TryParse = TryParse;
        m_methodHolderMap.Add(typeof(float), floatMethodHolder);

        MethodHolder<bool> boolMethodHolder = new MethodHolder<bool>();
        boolMethodHolder.TryParse = TryParse;
        m_methodHolderMap.Add(typeof(bool), boolMethodHolder);

        MethodHolder<string> stringMethodHolder = new MethodHolder<string>();
        stringMethodHolder.TryParse = TryParse;
        m_methodHolderMap.Add(typeof(string), stringMethodHolder);
    }

    public static T Add<T>(T a, T b)
    {
        Type type = typeof (T);
        IMethodHodler holder;
        if (m_methodHolderMap.TryGetValue(type, out holder))
        {
            MethodHolder<T> tHolder = holder as MethodHolder<T>;
            if (tHolder != null)
            {
                if (tHolder.Add != null)
                {
                    return tHolder.Add(a, b);
                }
            }
        }
        throw new Exception("Not Match Add Method");
        return default(T);
    }

    public static T Minus<T>(T a, T b)
    {
        Type type = typeof(T);
        IMethodHodler holder;
        if (m_methodHolderMap.TryGetValue(type, out holder))
        {
            MethodHolder<T> tHolder = holder as MethodHolder<T>;
            if (tHolder != null)
            {
                if (tHolder.Minus != null)
                {
                    return tHolder.Minus(a, b);
                }
            }
        }
        throw new Exception("Not Match Minus Method");
        return default(T);
    }

    public static T Multiply<T>(T a, T b)
    {
        Type type = typeof(T);
        IMethodHodler holder;
        if (m_methodHolderMap.TryGetValue(type, out holder))
        {
            MethodHolder<T> tHolder = holder as MethodHolder<T>;
            if (tHolder != null)
            {
                if (tHolder.Multiply != null)
                {
                    return tHolder.Multiply(a, b);
                }
            }
        }
        throw new Exception("Not Match Multiply Method");
        return default(T);
    }

    public static T Devide<T>(T a, T b)
    {
        Type type = typeof(T);
        IMethodHodler holder;
        if (m_methodHolderMap.TryGetValue(type, out holder))
        {
            MethodHolder<T> tHolder = holder as MethodHolder<T>;
            if (tHolder != null)
            {
                if (tHolder.Devide != null)
                {
                    return tHolder.Devide(a, b);
                }
            }
        }
        throw new Exception("Not Match Devide Method");
        return default(T);
    }

    public static bool TryParse<T>(string str, out T value)
    {
        Type type = typeof(T);
        IMethodHodler holder;
        if (m_methodHolderMap.TryGetValue(type, out holder))
        {
            MethodHolder<T> tHolder = holder as MethodHolder<T>;
            if (tHolder != null)
            {
                if (tHolder.TryParse != null)
                {
                    return tHolder.TryParse(str, out value);
                }
            }
        }
        throw new Exception("Not Match TryParse Method");
        return false;
    }

    #region byte

    private static byte Add(byte a, byte b)
    {
        return (byte)(a + b);
    }
    
    private static byte Minus(byte a, byte b)
    {
        return (byte)(a - b);
    }
    
    private static byte Multiply(byte a, byte b)
    {
        return (byte)(a * b);
    }
    
    private static byte Devide(byte a, byte b)
    {
        return (byte)(a / b);
    }
    
    private static bool TryParse(string str, out byte value)
    {
        return byte.TryParse(str, out value);
    }

    #endregion

    #region short
    
    private static short Add(short a, short b)
    {
        return (short)(a + b);
    }
    
    private static short Minus(short a, short b)
    {
        return (short)(a - b);
    }
    
    private static short Multiply(short a, short b)
    {
        return (short)(a * b);
    }
    
    private static short Devide(short a, short b)
    {
        return (short)(a / b);
    }
    
    private static bool TryParse(string str, out short value)
    {
        return short.TryParse(str, out value);
    }

    #endregion

    #region int
    
    private static int Add(int a, int b)
    {
        return a + b;
    }
    
    private static int Minus(int a, int b)
    {
        return a - b;
    }
    
    private static int Multiply(int a, int b)
    {
        return a * b;
    }
    
    private static int Devide(int a, int b)
    {
        return a / b;
    }
    
    private static bool TryParse(string str, out int value)
    {
        return int.TryParse(str, out value);
    }

    #endregion

    #region float
    
    private static float Add(float a, float b)
    {
        return a + b;
    }
    
    private static float Minus(float a, float b)
    {
        return a - b;
    }
    
    private static float Multiply(float a, float b)
    {
        return a * b;
    }
    
    private static float Devide(float a, float b)
    {
        return a / b;
    }
    
    private static bool TryParse(string str, out float value)
    {
        return float.TryParse(str, out value);
    }

    #endregion

    #region double
    
    private static double Add(double a, double b)
    {
        return a + b;
    }
    
    private static double Minus(double a, double b)
    {
        return a - b;
    }
    
    private static double Multiply(double a, double b)
    {
        return a * b;
    }
    
    private static double Devide(double a, double b)
    {
        return a / b;
    }
    
    private static bool TryParse(string str, out double value)
    {
        return double.TryParse(str, out value);
    }

    #endregion

    #region bool
    
    private static bool TryParse(string str, out bool value)
    {
        return bool.TryParse(str, out value);
    }

    #endregion

    #region string
    
    private static bool TryParse(string str, out string value)
    {
        value = str;
        if (value == null)
        {
            value = string.Empty;
        }
        return true;
    }

    #endregion
}
