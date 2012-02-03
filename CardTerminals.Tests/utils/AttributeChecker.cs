using System;

namespace Wiffzack.Devices.CardTerminals.Tests.utils
{
	/// <summary>
	/// Attribute checker.
	/// </summary>
	public class AttributeChecker
	{
		/// <summary>
		/// Contains all fields (i.e. their names) that should have int values.
		/// </summary>
		string[] intFields;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Wiffzack.Devices.CardTerminals.Tests.utils.AttributeChecker"/> class.
		/// Sets intFields list.
		/// </summary>
		public AttributeChecker ()
		{
			
		}
		
		/// <summary>
		/// Check the specified field and theValue.
		/// </summary>
		/// <param name='field'>
		/// Field.
		/// </param>
		/// <param name='theValue'>
		/// The value.
		/// </param>
		public static int check(string field, string theValue){
			return 0;
		}
	}
}