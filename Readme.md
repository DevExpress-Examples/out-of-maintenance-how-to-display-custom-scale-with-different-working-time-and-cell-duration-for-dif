<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128634568/13.1.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E5156)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomTimeScale.cs](./CS/WindowsFormsApplication1/CustomTimeScale.cs) (VB: [CustomTimeScale.vb](./VB/WindowsFormsApplication1/CustomTimeScale.vb))
* [Form1.cs](./CS/WindowsFormsApplication1/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsApplication1/Form1.vb))
<!-- default file list end -->
# How to display custom scale with different working time and cell duration for different week days


<p>This example demonstrates how to create a custom scale that meets the following requirements:</p><p>1. Working days: visible time from <strong>7AM </strong>till <strong>9PM</strong>, time cell duration is <strong>12 minutes</strong>;</p><p>2. Saturday: visible time from <strong>9AM </strong>till <strong>8PM</strong>, time cell duration is <strong>20 minutes</strong>;</p><p>3. Sunday: visible time from <strong>12AM </strong>till <strong>3PM</strong>, time cell duration is <strong>30 minutes</strong>.</p><p>This approach is based on overriding the <strong>Floor</strong>, <strong>GetNextDate </strong>and <strong>HasNextDate </strong>methods in a custom <strong>TimeScaleFixedInterval </strong>descendant.</p>

<br/>


