using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSport.API.Classes
{
    public class QueryParameters
    {
        const int _maxSitze = 100;
        private int _size = 50;

        public int Page { get; set; }
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = Math.Min(_maxSitze, value);
            }
        }

        public string SortBy { get; set; } = "Id";

        private string _sortOrder = "asc";
        public string SortOrder { 
            get
            {
                return _sortOrder;
            }
            set
            { 
                if (value == "asc" || value == "desc")
                {
                    _sortOrder = value;
                }
            }
        }
    }
}
