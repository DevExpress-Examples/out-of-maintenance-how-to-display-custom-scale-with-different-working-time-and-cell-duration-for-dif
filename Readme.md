# How to display custom scale with different working time and cell duration for different week days


<p>This example demonstrates how to create a custom scale that meets the following requirements:</p><p>1. Working days: visible time from <strong>7AM </strong>till <strong>9PM</strong>, time cell duration is <strong>12 minutes</strong>;</p><p>2. Saturday: visible time from <strong>9AM </strong>till <strong>8PM</strong>, time cell duration is <strong>20 minutes</strong>;</p><p>3. Sunday: visible time from <strong>12AM </strong>till <strong>3PM</strong>, time cell duration is <strong>30 minutes</strong>.</p><p>This approach is based on overriding the <strong>Floor</strong>, <strong>GetNextDate </strong>and <strong>HasNextDate </strong>methods in a custom <strong>TimeScaleFixedInterval </strong>descendant.</p>

<br/>


