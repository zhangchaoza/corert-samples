namespace CustomServerDemo.CustomFtpServer
{
    using System;
    using System.Collections.Generic;
    using FubarDev.FtpServer.Commands;

    public class SimpleFtpCommandHandlerScanner : IFtpCommandHandlerScanner
    {
        public IEnumerable<IFtpCommandHandlerInformation> Search()
        {
            return new IFtpCommandHandlerInformation[]
            {
                new SimpleFtpCommandHandlerInformation(typeof(RetrCommandHandler),false,"RETR",false,true),
            };
        }
    }

    public class SimpleFtpCommandHandlerInformation : IFtpCommandHandlerInformation
    {
        public SimpleFtpCommandHandlerInformation(Type Type, bool IsExtensible, string Name, bool IsLoginRequired, bool IsAbortable)
        {
            this.Type = Type;
            this.IsExtensible = IsExtensible;
            this.Name = Name;
            this.IsLoginRequired = IsLoginRequired;
            this.IsAbortable = IsAbortable;
        }

        public Type Type { get; }

        public bool IsExtensible { get; }

        public string Name { get; }

        public bool IsLoginRequired { get; }

        public bool IsAbortable { get; }
    }

}
