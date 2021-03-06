﻿using System.Collections.Generic;
using System.Linq;

namespace BrightWire.TabularData.Analysis
{
    /// <summary>
    /// A collector that collects the frequency from a single column of a data table
    /// </summary>
    internal class FrequencyCollector : IRowProcessor, IFrequencyColumnInfo
    {
        readonly int _index;
        readonly Dictionary<string, ulong> _valueCount = new Dictionary<string, ulong>();
        ulong _total = 0;

        public FrequencyCollector(int index)
        {
            _index = index;
        }

        public int ColumnIndex
        {
            get
            {
                return _index;
            }
        }

        public int? NumDistinct { get { return _valueCount.Count; } }

        public IEnumerable<object> DistinctValues
        {
            get
            {
                return _valueCount.Select(kv => kv.Key);
            }
        }

        public IEnumerable<KeyValuePair<string, ulong>> Frequency
        {
            get
            {
                return _valueCount;
            }
        }

        public ulong Total { get { return _total; } }

        public bool Process(IRow row)
        {
            var val = row.GetField<string>(_index);
            if (_valueCount.TryGetValue(val, out ulong count))
                _valueCount[val] = count + 1;
            else
                _valueCount.Add(val, 1);
            ++_total;
            return true;
        }
    }
}
