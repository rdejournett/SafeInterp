using System.Collections.Generic;

namespace InterpConsoleApp
{
    class ReturnData
    {
        string _orderID; // unique ID FOR THIS ORDER
        string _orderName;
        string _commonOrderName;
        string _resultID;
        string _resultName;
        string _commonResultName;
        bool _isPositive;

        public string OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }
        public string OrderName
        {
            get { return _orderName; }
            set { _orderName = value; }
        }
        public string CommonOrderName
        {
            get { return _commonOrderName; }
            set { _commonOrderName = value; }
        }
        public string ResultID
        {
            get { return _resultID; }
            set { _resultID = value; }
        }
        public string ResultName
        {
            get { return _resultName; }
            set { _resultName = value; }
        }
        public string CommonResultName
        {
            get { return _commonResultName; }
            set { _commonResultName = value; }
        }
        public bool IsPositive
        {
            get { return _isPositive; }
            set { _isPositive = value; }
        }

    }
    class ReturnDataList
    {
        List<ReturnData> _dataList;
        public ReturnDataList()
        {
            _dataList = new List<ReturnData>();
        }

        public List<ReturnData> DataList
        {
            get { return _dataList; }
            set { _dataList = value; }
        }


    }
}
