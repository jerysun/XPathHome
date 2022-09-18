<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="/">
    <html>
      <xsl:apply-templates />
    </html>
  </xsl:template>
  <xsl:template match="item">
    <table>
      <td>
        <xsl:apply-templates select="./title"/>
      </td>
      <td>
        <xsl:apply-templates select="./link"/>
      </td>
    </table>
  </xsl:template>
  <xsl:template match="title">
    <xsl:value-of select="."/>
  </xsl:template>
  <xsl:template match="link">
    <xsl:variable name="url">
      <xsl:value-of select="." />
    </xsl:variable>
    <a href="{$url}">
      <xsl:value-of select="."/>
    </a>
  </xsl:template>
</xsl:stylesheet>