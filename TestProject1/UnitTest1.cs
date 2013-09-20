using System.IO;
using FixSpaz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var s = new SpazFile( "" );
			Assert.AreEqual( @"C:\Users\peter.hamberg\AppData\Roaming\minmaxgames\spacepiratesandzombies", SpazFile.BaseFolder );
		}

		[TestMethod]
		[DeploymentItem( "data.d" )]
		public void CRCworks()
		{
			var c = new Crc32();
			c.Update( System.IO.File.ReadAllBytes( @"C:\src\git\FixSpaz\TestProject1\data.d" ) );
			Assert.AreEqual( 1342032153, c.Value );
		}

		[TestMethod]
		[Ignore]
		public void Deny()
		{
			Permissions.DenyDelete();
		}

		[TestMethod]
		public void Hex()
		{
			ulong decValue = 2952935142;
			var hexValue = Maths.ToHex( decValue );
			Assert.AreEqual( "B00236E6", hexValue );
			var decAgain = Maths.ToDec( hexValue );
			Assert.AreEqual( 2952935142, decAgain );
		}
		[TestMethod]
		public void file()
		{
			var lines = System.IO.File.ReadAllLines( @"C:\src\git\FixSpaz\TestProject1\data.d" );
			var sf = new SpazFile( "" );
			sf.MakeInfoFile( lines, 500 );

			Assert.AreEqual( lines[8], "500" );
			Assert.AreEqual( "zaz2013 09 18 00 22 572117LUYTEN1.6050500", lines[9] );
		}

		[TestMethod]
		public void Inv()
		{
			File.Create( "aoeu" );
			File.Create( "aoeu2" );
			File.Move( "aoeu", "aoeu2" );
			Assert.AreEqual( Maths.ToDec( "00000000" ), Maths.Invert( Maths.ToDec( "FFFFFFFF" ) ) );
			Assert.AreEqual( Maths.ToDec( "FFFFFFFF" ), Maths.Invert( Maths.ToDec( "00000000" ) ) );
			Assert.AreEqual( Maths.ToDec( "B00236E6" ), Maths.Invert( Maths.ToDec( "4FFDC919" ) ) );
			Assert.AreEqual( Maths.ToDec( "162a3aef" ), Maths.Invert( Maths.ToDec( "e9d5c510" ) ) );

			Assert.AreEqual( -609007523, Maths.Cast( Maths.Invert( Maths.ToDec( "244CB7A3" ) ) ) );

			Assert.AreEqual( 371866351, Maths.Cast( Maths.ToDec( "162a3aef" ) ) );

		}
	}
}
