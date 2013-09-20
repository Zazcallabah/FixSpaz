using System;

namespace FixSpaz
{
	public class Maths
	{
		public static string ToHex( ulong dec )
		{
			return dec.ToString( "X" );
		}

		public static ulong ToDec( string hex )
		{
			return ulong.Parse( hex, System.Globalization.NumberStyles.HexNumber );
		}

		public static int ToDecInt32( string hex )
		{
			return Int32.Parse( hex, System.Globalization.NumberStyles.HexNumber );
		}

		public static ulong Invert( ulong num )
		{
			return 4294967295 - num;
		}

		public static int Cast( ulong num )
		{
			if( num > Int32.MaxValue )
				return Int32.MinValue + (int) ( num - Int32.MaxValue );
			return (int) num;
		}
	}
}