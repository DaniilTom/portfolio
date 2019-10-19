using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightMap.Attributes
{

    public class RequiredFromQueryActionConstraint : Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint
    {
        private readonly string _parameter;

        public RequiredFromQueryActionConstraint(string parameter)
        {
            _parameter = parameter;
        }

        public int Order => 999;

        public bool Accept(Microsoft.AspNetCore.Mvc.ActionConstraints.ActionConstraintContext context)
        {
            if (!context.RouteContext.HttpContext.Request.Query.ContainsKey(_parameter))
            {
                return false;
            }

            return true;
        }
    }

    public class RequiredFromQueryAttribute : FromQueryAttribute, Microsoft.AspNetCore.Mvc.ApplicationModels.IParameterModelConvention
    {
        public void Apply(Microsoft.AspNetCore.Mvc.ApplicationModels.ParameterModel parameter)
        {
            if (parameter.Action.Selectors != null && parameter.Action.Selectors.Any())
            {
                parameter.Action.Selectors.Last().ActionConstraints.Add(new RequiredFromQueryActionConstraint(parameter.BindingInfo?.BinderModelName ?? parameter.ParameterName));
            }
        }
    }
}
