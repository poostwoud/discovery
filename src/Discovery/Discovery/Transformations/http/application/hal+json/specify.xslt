<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

  <!-- root -->
  <xsl:template match="/">
    <xsl:apply-templates select="response/entity/body/root"/>
  </xsl:template>

  <!-- entity body -->
  <xsl:template match="response/entity/body/root">
    <dictionary name="root">
      <!-- links -->
      <xsl:apply-templates select="_links" />
      <!-- embedded -->
      <xsl:apply-templates select="_embedded"/>
      <!-- properties -->
      <!-- TODO:Use of apply templates possible? -->
      <xsl:call-template name="properties">
        <xsl:with-param name="this" select="*[not(self::_links) and not(self::_embedded)]"/>
      </xsl:call-template>
    </dictionary>
  </xsl:template>

  <!-- _links (ignore curies) -->
  <xsl:template match="_links">
    <list name="_links">
      <xsl:apply-templates select="*[not(self::curies)]"/>
    </list>
  </xsl:template>

  <xsl:template match="_links/*">
    <xsl:choose>
      <!-- curies or default array -->
      <xsl:when test="@type='array'">
        <xsl:apply-templates select="./*"/>
      </xsl:when>
      <!-- curies object -->
      <xsl:when test="@item and @type='object'">
        <string name="{name(.)}" action="read" transclude="false" relation="{@item}" prompt="{title}">
          <xsl:value-of select="href"/>
        </string>
      </xsl:when>
      <!-- default object -->
      <xsl:otherwise>
        <string name="item" action="read" transclude="false" relation="{name(.)}" prompt="{title}">
          <xsl:value-of select="./href"/>
        </string>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="_links/*/*">
    <xsl:choose>
      <!-- curies or default array -->
      <xsl:when test="@type='array'">
        <xsl:apply-templates select="./*"/>
      </xsl:when>
      <!-- curies object -->
      <xsl:when test="@item and @type='object'">
        <string name="{name(.)}" action="read" transclude="false" relation="{@item}" prompt="{title}">
          <xsl:value-of select="href"/>
        </string>
      </xsl:when>
      <!-- default object -->
      <xsl:otherwise>
        <string name="item" action="read" transclude="false" relation="{name(.)}" prompt="{title}">
          <xsl:value-of select="./href"/>
        </string>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- _embedded -->
  <xsl:template match="_embedded">
    <list name="_embedded">
      <xsl:apply-templates select="*"/>
    </list>
  </xsl:template>

  <xsl:template match="_embedded/*">
    <dictionary name="{name(.)}">
      <!-- links -->
      <xsl:apply-templates select="_links"/>
      <!-- embedded -->
      <xsl:apply-templates select="_embedded"/>
      <!-- properties -->
      <xsl:call-template name="properties">
        <xsl:with-param name="this" select="*[not(self::_links)]"/>
      </xsl:call-template>
    </dictionary>
  </xsl:template>

  <!-- properties -->
  <xsl:template name="properties">
    <xsl:param name="this"/>
    <xsl:for-each select="$this">
      <string name="{name()}">
        <xsl:value-of select="text()"/>
      </string>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
