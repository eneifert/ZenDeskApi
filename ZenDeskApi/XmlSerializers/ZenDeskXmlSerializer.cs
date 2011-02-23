#region License
//   Copyright 2010 John Sheehan
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using RestSharp.Extensions;
using RestSharp.Serializers;

namespace ZenDeskApi.XmlSerializers
{
	/// <summary>
	/// Default XML Serializer
	/// </summary>
	public class ZenDeskXmlSerializer :  ISerializer
	{
		/// <summary>
		/// Default constructor, does not specify namespace
		/// </summary>
		public ZenDeskXmlSerializer() {
			ContentType = "text/xml";
		}

		/// <summary>
		/// Specify the namespaced to be used when serializing
		/// </summary>
		/// <param name="namespace">XML namespace</param>
        public ZenDeskXmlSerializer(string @namespace)
        {
			Namespace = @namespace;
			ContentType = "text/xml";
		}

		/// <summary>
		/// Serialize the object as XML
		/// </summary>
		/// <param name="obj">Object to serialize</param>
		/// <returns>XML as string</returns>
		public string Serialize(object obj) {
			var doc = new XDocument();

			var t = obj.GetType();
			var name = t.Name;

			var options = t.GetAttribute<ZenDeskSerializationAttribute>();
			if (options != null) {
				name = options.TransformName(options.Name ?? name);
			}

			var root = new XElement(name.AsNamespaced(Namespace));

			Map(root, obj);

			if (RootElement.HasValue()) {
				var wrapper = new XElement(RootElement.AsNamespaced(Namespace), root);
				doc.Add(wrapper);
			}
			else {
				doc.Add(root);
			}

			return doc.ToString();
		}

		private void Map(XElement root, object obj) {
            if (obj == null)
                return;

			var objType = obj.GetType();

			var props = from p in objType.GetProperties()
						let indexAttribute = p.GetAttribute<ZenDeskSerializationAttribute>()
						where p.CanRead && p.CanWrite
						orderby indexAttribute == null ? int.MaxValue : indexAttribute.Index
						select p;

            // We have a value type as a node
            if (props.Count() == 0)
            {
                if (objType.IsPrimitive || objType.IsValueType || objType == typeof(string))
                {
                    root.SetValue(obj); // set the node value to the object value
                }
            } else {
		        var globalOptions = objType.GetAttribute<ZenDeskSerializationAttribute>();

		        foreach (var prop in props) {
		            var name = prop.Name;
		            var rawValue = prop.GetValue(obj, null);

		            if (rawValue == null) {
		                continue;
		            }

		            var value = GetSerializedValue(rawValue);
		            var propType = prop.PropertyType;

		            var useAttribute = false;
		            var settings = prop.GetAttribute<ZenDeskSerializationAttribute>();
		            if (settings != null) {
		                name = settings.Name.HasValue() ? settings.Name : name;
		                useAttribute = settings.Attribute;
		            }                  

		            var options = prop.GetAttribute<ZenDeskSerializationAttribute>();                   

		            if (options != null) {

                        if (options.Skip)
                            continue;

		                name = options.TransformName(name);
		            }
		            else if (globalOptions != null) {
		                name = globalOptions.TransformName(name);
		            }

		            var nsName = name.AsNamespaced(Namespace);
		            var element = new XElement(nsName);                                

		            if (propType.IsPrimitive || propType.IsValueType || propType == typeof(string)) {
		                if (useAttribute) {
		                    root.Add(new XAttribute(name, value));
		                    continue;
		                }

                        //ZenDesk can't handle uppercase bools
                        if (propType == typeof(bool) || propType == typeof(bool?))
                            element.Value = value.ToLower();
                        else
		                    element.Value = value;

		            }
                    else if (rawValue is IList)
                    {
                        if (((IList) rawValue).Count == 0)
                            continue;

                        element.Add(new XAttribute("type", "array")); 

                        var itemTypeName = options != null ? options.ListItemName ?? string.Empty : string.Empty;
                        foreach (var item in (IList) rawValue)
                        {
                            if (itemTypeName == "")
                            {
                                Type itemType = item.GetType();
                                var att = itemType.GetAttribute<ZenDeskSerializationAttribute>();

                                if (att != null && att.Name != null)
                                    itemTypeName = att.Name;
                                else                                                    
                                    itemTypeName = item.GetType().Name;
                            }
                            var instance = new XElement(itemTypeName);
                            Map(instance, item);
                            element.Add(instance);
                        }

                    }
                    else
                    {
                        Map(element, rawValue);
                    }


		            root.Add(element);
		        }
		    }
		}

		private string GetSerializedValue(object obj) {
			var output = obj;

			if (obj is DateTime) {
				// check for DateFormat when adding date props
				if (DateFormat.HasValue()) {
					output = ((DateTime)obj).ToString(DateFormat);
				}
			}
			// else if... if needed for other types

			return output.ToString();
		}

		/// <summary>
		/// Name of the root element to use when serializing
		/// </summary>
		public string RootElement { get; set; }
		/// <summary>
		/// XML namespace to use when serializing
		/// </summary>
		public string Namespace { get; set; }
		/// <summary>
		/// Format string to use when serializing dates
		/// </summary>
		public string DateFormat { get; set; }
		/// <summary>
		/// Content type for serialized content
		/// </summary>
		public string ContentType { get; set; }
	}
}