﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BrightWire.Source.TabularData.Manipulation
{
    class GenericConverter<T> : IConvertToType
    {
	    readonly TypeConverter _converter;
	    readonly T _fallback;

	    public GenericConverter(T fallback = default(T))
	    {
		    _converter = TypeDescriptor.GetConverter(typeof(T));
		    _fallback = fallback;
	    }

	    public (object convertedValue, bool wasSuccessful) ConvertValue(object value)
	    {
		    try
		    {
			    return (_converter.ConvertFrom(value), true);
		    }
		    catch
		    {
			    return (_fallback, false);
		    }
	    }
    }
}
