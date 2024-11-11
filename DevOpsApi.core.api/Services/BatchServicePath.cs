using DevOpsApi.core.api.Models;
using System.Data;

namespace DevOpsApi.core.api.Services
{
    public class BatchServicePath
    {
        private readonly ILogger<BatchServicePath> logger;

        private static FileAttachmentInfo _fileAttachmentInfo;

        private BatchServicePath()
        {

        }

        public BatchServicePath(ILogger<BatchServicePath> logger)
        {
            this.logger = logger;
        }

        private static BatchServicePath instance;

        public static BatchServicePath Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BatchServicePath();
                }
                return instance;
            }
        }

        internal FileAttachmentInfo FileAttachmentInfo
        {
            get
            {
                if (_fileAttachmentInfo == null)
                    _fileAttachmentInfo = new FileAttachmentInfo();
                return _fileAttachmentInfo;
            }
            set
            {
                _fileAttachmentInfo = value;
            }
        }
        public string GeneratePathByDocNoAndAttachmentType(string documentNo, string attachmentType, string SourcePath, DocumentType docType)
        {
            try
            {
                string destinationFileName = string.Empty;
                string SourceFileName = Path.GetFileName(SourcePath);
                string destinationPath = GenaratingPath(attachmentType, documentNo, SourceFileName, docType);
                return destinationPath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Here is an example of parameters passed to generate the path
        /// "SALES FRONT COUNTER", "SIP-010-50-04200608", "20241028084145-Signature.BMP", DocumentType.NumberAndLetter
        /// </summary>
        /// <param name="attachmentType"></param>
        /// <param name="documentNo"></param>
        /// <param name="SourceFileName"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public string GenaratingPath(string attachmentType, string documentNo, string SourceFileName, DocumentType documentType)
        {
            try
            {
                if (SetPathInformation(attachmentType) == false)
                {
                    throw new Exception("Prefix for phath not found");
                }
                string prefix = FileAttachmentInfo.AdditionalDes1;

                string configPath = FileAttachmentInfo.AttachmentPath;
                if (string.IsNullOrWhiteSpace(configPath))
                    throw new Exception("Destination Path Can not be generated");

                string path = PathGenerator.GeneratedFilePath(documentNo, documentType);
                if (string.IsNullOrWhiteSpace(path))
                    throw new Exception("Destination Path Can not be generated");

                string destinationFileName = documentNo + "_" + Guid.NewGuid() + "_" + SourceFileName;
                string fullPath = configPath + @"\" + prefix + @"\" + path + @"\" + destinationFileName;
                return fullPath;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private bool SetPathInformation(string attchmentType)
        {
            try
            {
                //DataSet ds = Common.service.GetAttachmentPathDetailsByType(attchmentType);// "PO SUBLETS"

                //if (ValidatePath(ds) == false)
                //{
                //    return false;
                //}

                //FileAttachmentInfo.AdditionalDes1 = ds.Tables[0].Rows[0]["AdditionalDesc1"].ToString();
                //FileAttachmentInfo.AttachmentPath = ds.Tables[1].Rows[0]["AttachmentPath"].ToString();
                //FileAttachmentInfo.AttchmentTypeID = ds.Tables[0].Rows[0]["ReferenceMappingID"].ToString();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public string GetAttachmentTypeID()
        {
            return FileAttachmentInfo.AttchmentTypeID;
        }

        private bool ValidatePath(DataSet ds)
        {
            if (ds.Tables.Count < 2) return false;

            if (ds.Tables[0] == null)
            {
                //MessageBox.Show("No Subfolder found for Attaching the file");
                return false;
            }

            string AddtionalDesc1 = ds.Tables[0].Rows[0]["AdditionalDesc1"].ToString();
            if (String.IsNullOrEmpty(AddtionalDesc1))
            {
                //MessageBox.Show("No SubFolder found for Attaching the file");
                return false;
            }


            if (ds.Tables[1] == null)
            {
                //MessageBox.Show("No Path found for Attaching the file");
                return false;
            }

            string attachmentPath = ds.Tables[1].Rows[0]["AttachmentPath"].ToString();
            if (String.IsNullOrEmpty(attachmentPath))
            {
                //MessageBox.Show("No Path found for Attaching the file");
                return false;
            }

            return true;

        }
    }

}
