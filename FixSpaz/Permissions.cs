using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FixSpaz
{
	public static class Permissions
	{
		static FileSystemAccessRule MakeNewDenyDeleteRule()
		{
			return new FileSystemAccessRule(
				new SecurityIdentifier( "BU" ),
				FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.Delete,
				InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
				PropagationFlags.None, AccessControlType.Deny );
		}

		public static void DenyDelete()
		{
			var info = new DirectoryInfo( SpazFile.SaveFolder );
			var sec = info.GetAccessControl();
			sec.AddAccessRule( MakeNewDenyDeleteRule() );
			info.SetAccessControl( sec );
		}

		public static void RemoveDenyDelete()
		{
			var info = new DirectoryInfo( SpazFile.SaveFolder );
			var sec = info.GetAccessControl();
			sec.RemoveAccessRuleSpecific( MakeNewDenyDeleteRule() );
			info.SetAccessControl( sec );
		}
	}
}