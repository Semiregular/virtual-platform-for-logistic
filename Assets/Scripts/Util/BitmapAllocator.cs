using System;
using System.Collections;


namespace Util
{
    public class BitmapAllocator
    {
        private BitArray BitMap
        {
            get;
            set;
        }

        private int MaxId
        {
            get;
            set;
        }

        public BitmapAllocator(int maxId)
        {
            MaxId = maxId;
            BitMap = new BitArray(maxId + 1, false); 
        }

        public int Allocate()
        {
            for (int i = 0; i <= MaxId; i++)
            {
                if (!BitMap.Get(i))
                {
                    BitMap.Set(i, true);
                    return i;
                }
            }
            throw new InvalidOperationException("No available ID left.");
        }


        public void Free(int id)
        {
            if (id >= 0 && id <= MaxId)
            {
                BitMap.Set(id, false); 
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID out of range.");
            }
        }
    }
}
