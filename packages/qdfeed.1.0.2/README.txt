#####################################################################################################
README: Quick and Dirty Feed Parser 1.01

1. About Quick and Dirty Feed Parser
2. Features
3. License Terms (MS-PL)
4. Contributing to Quick and Dirty Feed Parser

Developed by Aaron Stannard (http://www.aaronstannard.com/) (Twitter: @Aaronontheweb)
If you'd like to contribute to the project, join us at CodePlex! http://qdfeed.codeplex.com/

#####################################################################################################

1. About Quick and Dirty Feed Parser

What's Quick and Dirty Feed Parser for?
Quick and Dirty Feed Parser is a lightweight .NET library designed to give developers an agnostic way of parsing RSS 2.0 and Atom 1.0 XML syndication formats. QD Feed Parser parses both Atom and RSS feeds into business objects with common interfaces which expose the substance of the feeds - that way you can get to the business of syndicating content without worrying about what format it's in.
Quick and Dirty Feed Parser works with .NET 4.0, Silverlight 4, and Windows Phone 7.

Why should I use Quick and Dirty Feed Parser?
Because it's awesome. QD Feed Parser spares you the banality known as XML parsing. It has a straightforward contract with the developers who use this library:

Pass the URI of a valid RSS/Atom feed to any IFeedFactory object's .CreateFeed method. 
Receive a fully populated IFeed object in return, which has all of the same members regardless of whether or not it's an RSS or an Atom feed. 
Do anything which violates this contact, promptly receive an Exception in return.

That's it. You send the parser the URI of a valid RSS 2.0 / Atom 1.0 feed and get a simple, populated .NET object in return. No XPath, no LINQ-to-XML, no dicking around with XML namespaces, no XML whatsoever. It's easy.


#####################################################################################################

2. Quick and Dirty Feed Parser Features

Here's a quick summary of some of the features you might in Quick and Dirty Feed Parser:
	-Format-agnostic parsing of Atom 1.0 and RSS 2.0 feeds;
	-Seamless consumption of feeds over Http and the file system;
	-Synchronous and Asynchronous methods for querying feeds;
	-Low memory consumption RSS/Atom objects that are easy to use;
	-Easily serializable classes built to work with any type of storage, including IsolatedStorage; and
	-Simple, simple, simple interfaces which abstract away 100% of the XML from your feeds. No more parsing. Ever.

If you'd like to learn more, check out our documentation: http://qdfeed.codeplex.com/documentation

#####################################################################################################

3. Quick and Dirty Feed Parser License (MS-PL)

Microsoft Public License (Ms-PL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions

The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the software.

A "contributor" is any person that distributes its contribution under this license.

"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights

(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement. 

#####################################################################################################

4. Contributing to Quick and Dirty Feed Parser

We'd love to have you contribute to the project and make it better. You can contribute to the project by going to http://qdfeed.codeplex.com/ 
and joining us on the discussion board, creating issues, or even better - forking the repository and committing changesets!

Here's the software you're going to need to modify Quick and Dirty Feed Parser:
	1. Visual Studio 2010 and above
	2. TortoiseHg (for Mercurial source control)
	3. TortoiseMerge (used by ApprovalTests for executing some of our test cases.)

If you have any questions about the project, please ask them on our discussion board on CodePlex or feel free to reach out to me at http://www.aaronstannard.com/ !

#####################################################################################################