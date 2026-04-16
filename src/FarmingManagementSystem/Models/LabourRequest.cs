namespace FarmingManagementSystem.Models
{
    public class LabourRequest
    {
        private int requestId;
        private string requestType;
        private string requestDescription;
        private string requestStatus;

        public int RequestId
        {
            get { return requestId; }
            set { requestId = value; }
        }

        public string RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        public string RequestDescription
        {
            get { return requestDescription; }
            set { requestDescription = value; }
        }

        public string RequestStatus
        {
            get { return requestStatus; }
            set { requestStatus = value; }
        }

        public LabourRequest()
        {
            requestId = 0;
            requestType = "";
            requestDescription = "";
            requestStatus = "";
        }

        public LabourRequest(int requestId, string requestType, string requestDescription, string requestStatus)
        {
            this.requestId = requestId;
            this.requestType = requestType;
            this.requestDescription = requestDescription;
            this.requestStatus = requestStatus;
        }
    }
}