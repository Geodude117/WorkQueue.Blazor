using DomainData.Models.QuestionModels;
using DomainData.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WorkQueue.Blazor.Helpers
{
    public class CustomMapper
    {
        public object Map(List<IDomainInfoViewModels> list, string className)
        {
            var newClass = this.CreateClass(className);
            newClass = MapItem(list, newClass);

            return newClass;
        }

        private object CreateClass(string className)
        {
            var newClassInstance = System.Activator.CreateInstance(Type.GetType(className));
            return newClassInstance;
        }

        public object MapItem(List<IDomainInfoViewModels> list, object Tmodel)
        {
            try
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
                                case "DropdownType":
                                    DropdownQuestion dropDownQuestion = (DropdownQuestion)domainItem.Question;
                                    property.SetValue(Tmodel, dropDownQuestion.Value);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                var x = ex.Message;
            }
           

            return Tmodel;
        }


        public List<IDomainInfoViewModels> ReMapItemToDynamicList(List<IDomainInfoViewModels> list, object Tmodel)
        {
            try
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
                                    txtQuestion.Value = (string)property.GetValue(Tmodel, null);
                                    break;
                                case "BoolType":
                                    BoolQuestion boolQuestion = (BoolQuestion)domainItem.Question;
                                    boolQuestion.Value = (bool)property.GetValue(Tmodel, null);
                                    break;
                                case "IntType":
                                    IntQuestion intQuestion = (IntQuestion)domainItem.Question;
                                    intQuestion.Value = (int)property.GetValue(Tmodel, null);
                                    break;
                                case "DateTimeType":
                                    DateTimeQuestion dateTimeQuestion = (DateTimeQuestion)domainItem.Question;
                                    dateTimeQuestion.Value = (DateTime)property.GetValue(Tmodel, null);
                                    break;
                                case "DropdownType":
                                    DropdownQuestion dropDownQuestion = (DropdownQuestion)domainItem.Question;
                                    dropDownQuestion.Value = (string)property.GetValue(Tmodel, null);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                var x = ex.Message;
            }

            return list;
        }



        public object MapProperty(object Tmodel, string propertyName, object propertyValue)
        {
            Type myType = Tmodel.GetType();

            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo property in props)
            {
                if (property.Name == propertyName)
                {
                    property.SetValue(Tmodel, propertyValue);
                    break;
                }
            }

            return Tmodel;
        }

        public object GetProperty(object Tmodel, string propertyName)
        {
            Type myType = Tmodel.GetType();

            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo property in props)
            {
                if (property.Name == propertyName)
                {
                    object propValue = property.GetValue(Tmodel, null);
                    return propValue;
                }
            }
            return null;
        }


    }
}
