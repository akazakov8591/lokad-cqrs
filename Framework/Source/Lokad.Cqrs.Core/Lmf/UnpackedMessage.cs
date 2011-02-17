#region (c) 2010 Lokad Open Source - New BSD License 

// Copyright (c) Lokad 2010, http://www.lokad.com
// This code is released as Open Source under the terms of the New BSD Licence

#endregion

using System;
using System.Collections.Generic;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Deserialized message representation
	/// </summary>
	public class UnpackedMessage
	{
		/// <summary>
		/// Type of the contract behind the message
		/// </summary>
		public readonly Type ContractType;
		
		/// <summary>
		/// Available message attributes
		/// </summary>
		public readonly MessageAttributesContract Attributes;
		/// <summary>
		/// Message content
		/// </summary>
		public readonly object Content;
		
		readonly IDictionary<string, object> _dynamicState = new Dictionary<string, object>();

		public UnpackedMessage(MessageAttributesContract attributes, object content, Type contractType)
		{
		
			ContractType = contractType;
			Attributes = attributes;
			Content = content;
		}

		public Maybe<TValue> GetState<TValue>()
		{
			return _dynamicState
				.GetValue(typeof(TValue).Name)
				.Convert(o => (TValue)o);
		}

		public UnpackedMessage WithState<TValue>(TValue value)
		{
			_dynamicState.Add(typeof(TValue).Name, value);
			return this;
		}
		
		public override string ToString()
		{
			return Content == null ? "NULL" : Content.ToString();
		}
	}
}