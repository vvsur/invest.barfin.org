namespace ExchangeSharp
{
	public sealed partial class ExchangeNDAXAPI
	{
		class AuthenticateResult
		{
			public bool Authenticated { get; set; }
			public int TestModelId { get; set; }
			public string Token { get; set; }
			public string AccountId { get; set; }
			public string OMSId { get; set; }
		}
	}
}
