using System;
using System.IO;
using GestionServer.Model;

namespace GestionServer
{
    public interface IController
    {
        Response parser(User user, Stream stream);
    }
}