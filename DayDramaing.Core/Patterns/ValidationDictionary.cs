using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public class ValidationDictionary : IModelState
    {
        private Dictionary<string, string> _Dictionary;
        public Dictionary<string, string> Dictionary { get { return _Dictionary; } }
        public ValidationDictionary()
        {
            _Dictionary = new Dictionary<string, string>();
            
        }
        public void AddError(string key, string errorMessage)
        {
            if (_Dictionary.ContainsKey(key))
            {
                _Dictionary[key] = errorMessage;
            }
            else
            {
                _Dictionary.Add(key, errorMessage);
            }
        }
        public bool IsValid
        {
            get { return _Dictionary.Count == 0; }
        }

        public string GetErrors()
        {
            var sb = new StringBuilder();
            foreach (var item in this.Dictionary)
            {
                sb.AppendLine(string.Format("{0}: {1}", item.Key, item.Value));
            }
            return sb.ToString();
        }

        public string Username { get; set; }

        public IPrincipal User { get; set; }
    }
}
