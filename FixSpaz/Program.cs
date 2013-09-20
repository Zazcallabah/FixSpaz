using System;
using System.IO;
using System.Linq;

namespace FixSpaz
{
	class Program
	{

		static void Main( string[] args )
		{
			var name = args.FirstOrDefault();

			if( string.IsNullOrEmpty( name ) )
			{
				Console.WriteLine( "Enter save file label" );
				name = Console.ReadLine();
			}

			var sf = new SpazFile( name );
			if( !File.Exists( sf.SaveDataFile ) )
			{
				Permissions.DenyDelete();
				Console.WriteLine( "Missing decompiled save file.\r\nOpen SPAZ and save your game, then press enter" );
				Console.ReadKey();
				Permissions.RemoveDenyDelete();
				Console.WriteLine( "Edit the save file to your liking.\r\nWhen done, press enter" );
				Console.ReadKey();
			}

			sf.BackupSettings();
			File.Copy( sf.SaveDataFile, SpazFile.UncompiledSettings );
			Console.WriteLine( "Prepared for compile. Please start-exit SPAZ, then press enter" );
			Console.ReadLine();
			try
			{
				sf.RemoveIfExists( sf.CompiledSaveDataFile );
			}
			catch( UnauthorizedAccessException )
			{
				Console.WriteLine( "You forgot to remove the deny delete permission, plz fix." );
				Console.ReadKey();
				sf.RemoveIfExists( sf.CompiledSaveDataFile );
			}
			catch( IOException )
			{
				Console.WriteLine( "File lock?" );
				Console.ReadKey();
				sf.RemoveIfExists( sf.CompiledSaveDataFile );
			}
			File.Move( SpazFile.CompiledSettings, sf.CompiledSaveDataFile );
			sf.RestoreSettings();
			Console.Write( "Compiled ok, calculating hash... " );
			var hash = sf.CalculateCrc32();
			var castinvert = Maths.Cast( Maths.Invert( (ulong) hash ) );
			Console.WriteLine( castinvert );
			Console.Write( "Applying hash to info file... " );
			var lines = File.ReadAllLines( sf.SaveInfoFile );
			sf.MakeInfoFile( lines, castinvert );
			File.WriteAllLines( sf.SaveInfoFile, lines );
			Console.WriteLine( "Done" );
		}
	}
}
