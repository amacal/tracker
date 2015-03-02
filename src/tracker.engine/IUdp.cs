using System;

namespace tracker
{
	public interface IUdp
	{
		IEndpoint Resolve(string hostname, int port);

		IUdpSession CreateSession(IEndpoint endpoint);
	}

	public interface IUdpSession : IDisposable
	{
		void Send(IUdpRequest request);

		IUdpResponse Receive();
	}

	public interface IUdpRequest
	{
		int Length { get; }

		byte[] ToBytes();
	}

	public interface IUdpResponse
	{
		byte[] ToBytes();

		int Length { get; }
	}
}