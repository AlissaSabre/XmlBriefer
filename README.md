XmlBriefer
===

A tool to generate a structure briefing of foreign XML files.

## WARNING

This is a preliminary work and may or may not work appropriately.

## What is it?

XmlBriefer is a small tool
that helps understanding the structure (schema/DTD) of foreign XML files.
In this context,
a _foreign_ XML file means an XML document instance
whose schema/DTD you don't know.

XML is used widely in many contexts recently,
and you may require reading/producing an XML document.
However, the interface specification just says _XML_ and no schema/DTD is given,
and you need to make some guess work over several examples.
With a few short examples,
it may not be easy to catch the entire rules of the document,
but it gets harder to analyze and understand a lot of longer examples.

XmlBriefer helps you when you guess the schema/DTD from examples
by providing some brief structure of the given XML documents.
