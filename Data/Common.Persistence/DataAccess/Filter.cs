using System.Collections.Generic;
using System.Linq;

namespace Common.Persistence.DataAccess
{
    public class Filter
    {
        public string StoredProcedureName { get; }
        public List<BaseParameter> Parameters { get; }
        public bool HasParameters => Parameters.Any();

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        public Filter(string storedProcedureName)
        {
            StoredProcedureName = storedProcedureName;
            Parameters = new List<BaseParameter>();
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="paramValue">The param</param>
        public void AddParameter(BaseParameter paramValue)
        {
            Parameters.Add(paramValue);
        }
    }
}
