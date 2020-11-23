using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using NModbus.Logging;
using NModbus.Extensions;
using NModbus.Message;
using NModbus.Unme.Common;
using NModbus.Utility;

namespace NModbus.IO
{
    internal class ModbusRtuOverIpTransport : ModbusIpTransport
    {
        public const int RequestFrameStartLength = 7;

        public const int ResponseFrameStartLength = 4;

        internal ModbusRtuOverIpTransport(IStreamResource streamResource, IModbusFactory modbusFactory, IModbusLogger logger)
            : base(streamResource, modbusFactory, logger)
        {
        }

        internal int RequestBytesToRead(byte[] frameStart)
        {
            byte functionCode = frameStart[1];

            IModbusFunctionService service = ModbusFactory.GetFunctionServiceOrThrow(functionCode);

            return service.GetRtuRequestBytesToRead(frameStart);
        }

        internal int ResponseBytesToRead(byte[] frameStart)
        {
            byte functionCode = frameStart[1];

            if(functionCode > Modbus.ExceptionOffset) { return 1; }

            IModbusFunctionService service = ModbusFactory.GetFunctionServiceOrThrow(functionCode);

            return service.GetRtuResponseBytesToRead(frameStart);
        }

        public virtual byte[] Read(int count)
        {
            byte[] frameBytes = new byte[count];
            int numBytesRead = 0;

            while (numBytesRead != count)
            {
                numBytesRead += StreamResource.Read(frameBytes, numBytesRead, count - numBytesRead);
            }

            return frameBytes;
        }

        public override IModbusMessage ReadResponse<T>()
        {
            byte[] frame = ReadResponse();

            Logger.LogFrameRx(frame);

            return CreateResponse<T>(frame);
        }

        private byte[] ReadResponse()
        {
            byte[] frameStart = Read(ResponseFrameStartLength);
            byte[] frameEnd = Read(ResponseBytesToRead(frameStart));
            byte[] frame = frameStart.Concat(frameEnd).ToArray();

            return frame;
        }

        public override byte[] ReadRequest()
        {
            byte[] frameStart = Read(RequestFrameStartLength);
            byte[] frameEnd = Read(RequestBytesToRead(frameStart));
            byte[] frame = frameStart.Concat(frameEnd).ToArray();

            Logger.LogFrameRx(frame);

            return frame;
        }

        public override byte[] BuildMessageFrame(IModbusMessage message)
        {
            byte[] pdu = message.MessageFrame;
            byte[] crc = ModbusUtility.CalculateCrc(message.MessageFrame);
            var messageBody = new MemoryStream(pdu.Length + crc.Length);

            messageBody.Write(pdu, 0, pdu.Length);
            messageBody.Write(crc, 0, crc.Length);

            return messageBody.ToArray();
        }
    }
}
