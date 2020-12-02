using DomainData.Models.QuestionModels;
using DomainData.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WorkQueue.Blazor.Helpers
{
    public class CustomMapper
    {
        public object Map(DomainData.Models.ViewModels.IDomainViewModel model)
        {
            string ClassName = model.DomainGroup.ClassMapping;
            var newClass = this.CreateClass(model.DomainGroup.ClassMapping);

            newClass = MapItem(model.DomainInfoViewModels, newClass);

            return newClass;
        }

        private object CreateClass(string className)
        {
            var x = System.Activator.CreateInstance(Type.GetType(className));
            return x;
        }

        public object MapItem(List<IDomainInfoViewModels> list, object Tmodel)
        {
            Type myType = Tmodel.GetType();

            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (var domainItem in list)
            {
                foreach (PropertyInfo property in props)
                {

                    if (domainItem.DomainInformation.PropertyMapping == property.Name)
                    {
                        switch (domainItem.DomainType.TypeName)
                        {
                            case "StringType":
                                TextQuestion txtQuestion = (TextQuestion)domainItem.Question;
                                property.SetValue(Tmodel, txtQuestion.Value);
                                break;
                            case "BoolType":
                                BoolQuestion boolQuestion = (BoolQuestion)domainItem.Question;
                                property.SetValue(Tmodel, boolQuestion.Value);
                                break;
                            case "IntType":
                                IntQuestion intQuestion = (IntQuestion)domainItem.Question;
                                property.SetValue(Tmodel, intQuestion.Value);
                                break;
                            case "DateTimeType":
                                DateTimeQuestion dateTimeQuestion = (DateTimeQuestion)domainItem.Question;
                                property.SetValue(Tmodel, dateTimeQuestion.Value);
                                break;
                        }
                    }
                }
            }

            return Tmodel;
        }
    }
}
