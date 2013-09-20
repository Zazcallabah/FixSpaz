using System;
using System.IO;

namespace FixSpaz
{
	public class SpazFile
	{
		readonly string _name;

		public SpazFile( string name )
		{
			_name = name;
		}

		public static string BaseFolder
		{
			get
			{
				return Path.Combine(
					Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
					@"minmaxgames\spacepiratesandzombies" );
			}
		}
		public void MakeInfoFile( string[] original, int newhash )
		{
			original[8] = newhash.ToString( System.Globalization.CultureInfo.InvariantCulture );
			var combinedhash = "";
			for( var i = 0; i < 9; i++ )
			{
				combinedhash = combinedhash + original[i];
			}
			original[9] = combinedhash;
		}
		public long CalculateCrc32()
		{
			var hash = new Crc32();
			hash.Update( File.ReadAllBytes( CompiledSaveDataFile ) );
			return hash.Value;
		}

		public string SaveInfoFile { get { return Path.Combine( SaveFolder, "si_" + _name + ".cs" ); } }
		public string SaveDataFile { get { return Path.Combine( SaveFolder, "sg_" + _name + ".cs" ); } }
		public string CompiledSaveDataFile { get { return Path.Combine( SaveFolder, "sg_" + _name + ".cs.dso" ); } }

		public static string SaveFolder { get { return Path.Combine( BaseFolder, "b_save" ); } }
		public static string SettingsFolder { get { return Path.Combine( BaseFolder, "b_settings" ); } }
		public static string UncompiledSettings { get { return Path.Combine( SettingsFolder, "spz_settings.cs" ); } }
		public static string CompiledSettings { get { return Path.Combine( SettingsFolder, "spz_settings.cs.dso" ); } }

		public void BackupSettings()
		{
			BackupFile( "spz_settings.cs" );
			BackupFile( "spz_settings.cs.dso" );
		}

		public void BackupFile( string file )
		{
			var path = Path.Combine( SettingsFolder, file );
			if( File.Exists( path ) )
			{
				var backup = path + ".backup";
				RemoveIfExists( backup );
				File.Move( path, backup );
			}
		}

		public void RestoreFile( string file )
		{
			var backup = Path.Combine( SettingsFolder, file + ".backup" );
			if( File.Exists( backup ) )
			{
				var path = Path.Combine( SettingsFolder, file );
				RemoveIfExists( path );
				File.Move( backup, path );
			}
		}

		public void RemoveIfExists( string path )
		{
			if( File.Exists( path ) )
				File.Delete( path );
		}

		public void RestoreSettings()
		{
			RestoreFile( "spz_settings.cs" );
			RestoreFile( "spz_settings.cs.dso" );
		}
	}
}