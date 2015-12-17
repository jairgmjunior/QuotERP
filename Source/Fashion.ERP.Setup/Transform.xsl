<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:strip-space elements="*"/>

  <!-- identity transform -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <!--<xsl:template match="wix:File[@Source='SourceDir\Web.config']">
    <xsl:copy>
      <xsl:attribute name="Permanent">yes</xsl:attribute>
      <xsl:copy-of select="@*"/>
    </xsl:copy>
  </xsl:template>-->


  <xsl:template match="wix:Component[wix:File/@Source='SourceDir\Web.config']">
    <xsl:copy>
      <xsl:attribute name="Permanent">yes</xsl:attribute>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>