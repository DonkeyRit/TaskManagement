using System.Collections.Generic;

namespace Core.Database.ObjectReader

{
    public abstract class Reader
    {
        public abstract List<object> GetValue(int index, bool type);

        public abstract List<string[]> GetValues();

        public abstract void Close();
    }
}