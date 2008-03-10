using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioWrapper;

namespace VWDAddin
{
    public class VisioMaster
    {
        public static Document GetStencil(Documents documents)
        {
            try
            {
                return documents[Constants.StencilName];
            }
            catch
            {
                // The stencil is not in the collection; open it as a docked stencil.
                return documents.OpenEx(Constants.StencilName, (short)VisOpenSaveArgs.visOpenDocked);
            }
        }

        private static int count = 0;
        public static Shape Drop(
            Document document,
            string masterNameU)
        {
            return Drop(document, masterNameU, ++count, count);
        }

        public static Shape Drop(
            Document document,
            string masterNameU,
            double pinX,
            double pinY)
        {
            Shape droppedShape = null;
            try
            {
                // Get a master from the stencil by its universal name.
                Document stencil = GetStencil(document.Application.Documents);
                Master master = stencil.Masters.get_ItemU(masterNameU);

                // Drop the master on the page
                droppedShape = document.Pages[1].Drop(master, pinX, pinY);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.Message);
            }

            return droppedShape;
        }

        public static Shape DropConnection(
            Shape source,
            Shape target,
            string masterNameU,
            ClassConnections sourcePoint,
            ClassConnections targetPoint)
        {
            VisioConnector connector = new VisioConnector(
                Drop(source.Document, masterNameU, 0, 0)
            );
            connector.SetSource(source, sourcePoint);
            connector.SetTarget(target, targetPoint);
            return connector.Shape;
        }
    }
}
