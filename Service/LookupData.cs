namespace InterpCheckSvc
{
    class LookupData
    {
        
        string _facilityName;
        string _orderID;
        string _labName;
        string _labCode;
        string _resultName;
        string _resultCode;
        string _resultValue;

        public string FacilityName
        {
            get { return _facilityName; }
            set { _facilityName = value; }
        }
        public string OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }
        public string LabName
        {
            get { return _labName; }
            set { _labName = value; }
        }
        public string LabCode
        {
            get { return _labCode; }
            set { _labCode = value; }
        }
        public string ResultName
        {
            get { return _resultName; }
            set { _resultName = value; }
        }
        public string ResultCode
        {
            get { return _resultCode; }
            set { _resultCode = value; }
        }
        public string ResultValue
        {
            get { return _resultValue; }
            set { _resultValue = value; }
        }



        public bool GetDataFromDatabase(string facilityName, string labName1, string resultName1, string resultValue1)
        {
            return true;
        }
    }
}
