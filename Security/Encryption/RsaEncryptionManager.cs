using Security.Contracts;
using System.Security.Cryptography;
using System;

using System.Text;
using System.Numerics;

namespace Security.Encryption
{
    public class RsaEncryptionManager : IRsaEncryptionManager
    {
        public byte[] GenerateKeys()
        {
            throw new NotImplementedException();
        }

        public void TestPubKeyDecrypt()
        {
           // using (var rsa = new RSACryptoServiceProvider(2048) )
           // {
           //     rsa.FromXmlString(

           //        // "<RSAKeyValue>"
           //        //    + "<Modulus>9zZBzXhq2GE2iDhwrjtI4goUARU2d2/R0TGZPiesbn6wOI7uNPePEhd3kaev8sa0Kb79S6oJdrD/0uf7FjUgEB6qtfC/gK3q0HofEFMzyAKyoJSqxWMd3s4bdBFYu9cWttHhNiwK0WbYjJMmUUUkRjkIVhPx6M6cQbKz45bEfl+OZUpC/JMMlUuIgQ4gqecKqdeV+de3Pk+hdTu5YgS0fPAu3WxiNBbFJ9l1rCyNkYDKdIH8GWGXon3Y70MT0P1vai+/VoKx9L2X3L2Dhl31zf1LTK7l1N/WIBYld/InA7ZtqLb/xJdWiNnoMUwFcoNCSCr3PEeMm9gk+g4bbWJfjQ==</Modulus>"
           //        //    + "<Exponent>AQAB</Exponent>"
           //        //+ "</RSAKeyValue>"
           // "<RSAKeyValue>"
	          //  +"<Modulus>9zZBzXhq2GE2iDhwrjtI4goUARU2d2/R0TGZPiesbn6wOI7uNPePEhd3kaev8sa0Kb79S6oJdrD/0uf7FjUgEB6qtfC/gK3q0HofEFMzyAKyoJSqxWMd3s4bdBFYu9cWttHhNiwK0WbYjJMmUUUkRjkIVhPx6M6cQbKz45bEfl+OZUpC/JMMlUuIgQ4gqecKqdeV+de3Pk+hdTu5YgS0fPAu3WxiNBbFJ9l1rCyNkYDKdIH8GWGXon3Y70MT0P1vai+/VoKx9L2X3L2Dhl31zf1LTK7l1N/WIBYld/InA7ZtqLb/xJdWiNnoMUwFcoNCSCr3PEeMm9gk+g4bbWJfjQ==</Modulus>"
	          //  +"<Exponent>AQAB</Exponent>"
	          //  +"<P>/5SbCiQgX4RyPZmtbETZmvRPxw1Kw1QM9/OtZb2fVqhSALz56v6rwlqGyXi0dk2wJQFw84BNfHqhaSPdyPxwjZaJXnBuFyI+wtkigCJBJND/L7ner+BjKO4J9v3agMxtv5J6pgAiU4803ihA11QBmQh9uEE7dO5gJxiLswP6fJc=</P>"
	          //  +"<Q>954ijX4Rdd9vd9GPDZOysDR+TTdAC6B2hfpogsoQLhxSP1cnzNxmA/xE1Hde0k8EFhjudf6aGGnCskeYaeneuDskhNjXnf97ZES7cFo6o/JXS1xGVbD9fmXWyrjVqizW6fOkDZzLYU5B+sjrXUMDrn1VGPJ2K1/pV99Td0kU9Xs=</Q>"
	          //  +"<DP>RNvL3bKYCkQL527VG5t9KVNzfwSkxPWLPO6pJAUvvdBBr7M6fka5DfcH45YiwNDziTTXMrO5rLT5cfNY2MKyrGMHhasy7gaq9CI+OlmARaQNbNVeGvKQpMFla+c/DH6Hfxq+8qSMmwi1TLl5psoaWBnCjXb9xuZGf6IMWWHUBec=</DP>"
	          //  +"<DQ>czUkJmjtfsZCeqEJyetaTBlgWlTGe6JhAt0LGy8gcBPFQKswXWR+IoSREbmoaHlTEWTwLf4TfCBY8dHV3BFwCo+Z4iVxzJU9t90yyIdymSz76Jg6MUxz5QdE9HUjFFZgd+FgBuVYyyE6GZC50V6Iq/qsSTsmN/AcBUJm9y0Nj1k=</DQ>"
	          //  +"<InverseQ>rcVwRlpUIJtu1148IHE7R/HqgJOJrYZ0JZez85a8OrKhpr+pZdCnlaci7hellMDpDY0QelQgW+3NR6jzuQwhAbfw5s7x1nzskpmS8/2zSkFnVK9/1bXvhWupqloR+25VteTMWQ9UgyAU1mdFByEO9X9sXiOuLkUQddXe9gMTXRA=</InverseQ>"
	          //  +"<D>zoPI3LjnqPMs9wcPOr3T2ODKbU0nPwduo+9nMQE7juLOm7DrVdwo7NglzsvitFFCWE1wlDDrzvd1/t5EZvziWBUGTw9bK0gejSI3qQ+YhlGan4MSVerDHUnYrVGAawr3sqoKFZMdRmlAJc8Xh3TXJMKoMCBhSjavWkLK/CkK5PWSRWw2CT6FOQZKjEOjs+i3ajhi3u3sbxnV5CS3IG1he/gS9p0lSVBe/o0t17jUXhw09PtEtT+P4vbl1qDkk6gCn6B8D6MPIcgR+6IR14PWVLud3LG5YylwEq/XMa7Jun/ndW78z3Vp1R7fspXhiDrprEivHSUBTpkIHXLRhEtCkQ==</D>"
           //+"</RSAKeyValue>");

           //    // rsa.ImportCspBlob(getPublicKeyParams() );

           //     // var msg = new sbyte[] { -40, 47, -63, 126, -44, -98, 8, -39, 116, -64, -83, -92, -124, 93, -7, -42, 80, 53, -28, 87, 68, -45, -71, -56, 51, 77, 42, -115, 84, 16, 60, -39, 62, -125, 66, -16, 100, -72, 29, 35, -77, 30, -61, -31, 49, 104, 122, -73, -116, 123, 92, -85, -118, 19, 118, -3, 1, 80, -27, -90, -106, 37, -90, 33, 103, -17, 96, -16, 96, 84, -6, 47, -105, 106, 122, -33, 41, 22, -9, 106, 39, -87, 42, 71, 5, -111, 98, -126, -72, 34, 88, -117, -112, 67, -21, -40, 22, -59, 58, -92, 62, 2, -128, 69, -112, 119, 27, -42, -61, 110, -105, 69, 20, -35, -81, 82, -8, -17, 86, 125, 64, -111, 9, 106, -9, -29, 70, 53, 115, -26, -72, 62, 0, 39, 0, -26, -93, 53, -127, -106, 33, 108, 39, -11, -115, 19, 31, -101, 123, 98, -77, 87, 41, -22, -127, 77, 65, 21, 40, -49, -106, 41, 100, -86, 115, 2, -25, 75, -53, 38, 22, 55, 18, 1, 15, -85, 104, 100, 10, 77, 1, -30, -111, -33, 80, -12, -121, 50, -8, 94, 92, 95, -47, 110, -106, 49, 119, -112, 38, 64, -105, 79, 61, 47, 24, -39, 81, 21, 44, 38, -57, 26, -88, -45, -36, -1, 112, 55, 43, 73, 83, -112, -32, 125, 110, -112, -56, 117, 71, 77, -118, -37, -37, 35, 91, -72, -118, 100, 21, -19, -25, -82, -83, -84, 126, -100, -69, 22, -86, 24, -103, 61, 23, -38, 69, 81 };
           //     var msg = Encoding.ASCII.GetBytes("test");
                
           //     var encryptedBytes = rsa.Encrypt(msg, false);

           //     var base64 = Convert.ToBase64String(encryptedBytes);

           //     var unencryptedMsg = rsa.Decrypt(encryptedBytes, false);
                

           //     int sadness = 100;
        }
    }
}
