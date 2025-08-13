using System;
using System.Collections.Generic;

namespace bananplaysshu.Tools {
	public static class ErrorCodeGenerator {
		private static int errorCode = 100;
		private static Dictionary<string, int> errorCodes = new Dictionary<string, int>();

		private static int GenerateNewCode() => errorCode++;

		public static string PrintNewErrorMessage(string errorName) {
			string errorMessage = $"An error has occured... Error code: {GenerateNewCode()}";
			errorCodes.Add(errorName, errorCode);
			return errorMessage;
		}

	}
}
