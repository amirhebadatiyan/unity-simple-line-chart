[![license](https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square)](https://github.com/dimmpixeye/InspectorFoldoutGroup/blob/master/LICENSE)

# unity-simple-line-chart
Easy to use, configurable line chart for unity game engine.

![alt text](https://github.com/amirhebadatiyan/unity-simple-line-chart/blob/main/ss_01.jpg)


Adding new record:

```csharp
  public class ClassName : MonoBehaviour
  {
    public Chart yourChart;
    
    public YourMethodToAddData()
    {
      yourChart.AddData(new TwoDimensionalData (valueX , ValueY),true);
    }
  }
```


