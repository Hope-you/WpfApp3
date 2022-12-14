## 布局 (容器)

> 各种Grid Panel等等,有不同特性

- `Grid`
  -  定义列`<Grid.ColumnDefinitions> <ColumnDefinition /> </Grid.ColumnDefinitions>`
  - 定义行 `<Grid.RowDefinitions> <RowDefinition />  </Grid.RowDefinitions>`
  - 子元素可以设置`Sapn` `<StackPanel  Grid.Row="1" Background="Gray"  Grid.ColumnSpan="2">`  默认是占一行一列，比如把grid设置了三行，可以设置`Grid.RowSpan="2"`让子元素占2行
- `StackPanel` 默认从上往下排布 右边距离不够，不会换行
  - `Orientation="Horizontal"` 修改排列方式为水平
- `WrapPanel`  默认从左往右排列，右边距离不够，自动转到下一行 （流式布局）
- `DockPanel`  和`StackPanel`有点像，多一个停靠的功能
  - 最后一个元素会默认居中  可以更改属性`LastChildFill="False"`取消居中 
  - 设置`DockerPanel`的`DockPanel.Dock="Bottom" `可以让其停靠的四个方向
- `UniformPanel` 会自动设置元素的宽高，和排列顺序，有点像`WrapPanel` ，但是`UniformPanel`会自动调整宽高，让其填充满，而不是剩余距离不够，自动转到下一行，换句话说，如果`UniformPanel`子元素切换到下一行，那上一行肯定是被填充满的。

## 样式 Style

- `<Window.Resources>   <Style TargetType="Button">   <Setter Property="FontSize" Value="18" />   </Style>   </Window.Resources>`
  - 样式是需要指定目标类型的，button、textbox等等，
  - `Setter`是设置器，需要通过键值对来指定样式和值
  - 元素本身样式的优先级大于样式

## 控件模板  `<Setter Property = "Template">` `<ControlTemplate>`

> 可以自定义控件的样式和结构

- 在`<Window.Resources>`里面定义`<ControlTemplate>`,指定类型和`x:Key`后可以给对用的控件使用,模板里面包含了控件的样式,组成,触发器,换言之,你可以把button改成textbox,反之亦可
- 有些控件是有`Content`属性的,在模板里面可以通过`ContentPresenter`来定义,  `Content`本身就是一个object类型的属性

## 数据模板  `<DataTemplate>`

> 后端获取数据,前端渲染减少代码量的同时给前后端解耦

- 在控件的`Template`标签下声明`<DataTemplate>`  这个模板就像是Vue里面的插槽,相当于一个容器,可以写上很多复杂的内容
- 后端给这个控件名字的`ItemSource`赋值
- 前端控件绑定后端数据的属性 `<TextBlock Text="{Binding index}" />` 
  - `index`就是后端对象的属性
  - `<Border Background="{Binding code}"`

## 数据绑定  (双向、单相、单次)

> 控件之间数据绑定,前后端数据的帮的绑定

- **控件之间数据绑定** `Text="{Binding ElementName=slider,Path=Value,Mode=OneWay}" />`
  - 这个`Path`就是需要绑定的控件的属性
  - `Mode`可以设置绑定模式,默认是 `TwoWay` 
    - `OneWay` 我绑定的目标属性可以改变'我'的值,我不能改变他
    - `OneWayToSource` '我'可以改变目标控件,他不能改变我
- 后端绑定,通过`this.DataContext`来绑定数据给前端

## 命令 `ICommand`

> 这个接口的目的是为了实现前端事件和后端方法实现解耦,不需要像事件驱动那样给前端修改事件名字,而是传一个继承了`ICommand`接口的类过去,这个类可以在构造函数里传一个Action过去,交给`ICommand`的`ICommand`方法. 绕是真的绕！！！



## 资源字典  `ResourceDictionary`

> 预先把样式、触发器写到字典里,然后在`App.xaml`里面引用这个`x:key` 就可以在所有的窗体中使用到预设的样式

- 创建资源字典就是一个xaml文件,直接在`ResourceDictionary`标签里面写`Style`就行了
- 在`App.xaml`中引用刚刚创建的资源字典
  - `<ResourceDictionary>` -> `<ResourceDictionary.MergedDictionaries>` 指定->`<ResourceDictionary Source="BaseStyle.xaml"/>`
- 指定之后这样使用`<Button Style="{DynamicResource DefaultButtonStyle}"`



## Prism框架 

> MVVM的框架，包含了依赖注入，将前端后端解耦，大致分为区域()导航和通知三个部分，先导入包或者安装Prism扩展可以直接创建解决方案，修改`App.xaml`的命名空间为Prism



### 区域、导航 `ContentControl`  `Navigation`

- 在父窗体中加入`ContentControl`标签,用来承载wpf的用户控件.
  - 引入`xmlns:prism="http://prismlibrary.com/"`命名空间后,给`ContentControl`标签设置`prism:RegionManager.RegionName="ContentRegion"`属性,名字是自定义的.
  - 在对应的ViewModels文件夹中对应ViewModel的构造函数中注入`IRegionManager regionManager`,`regionManager`有`Regions`这个集合,通过上一条的`RegionName`找到这个控件,执行`RequestNavigate(模块名)`让区域显示对应的模块.模块需要在`App.xaml`中的`RegisterTypes`方法里注入依赖`containerRegistry.RegisterForNavigation<View1>();`,注入依赖的方式不一样,调用的方式也不一样.
  - ViewModel里面需要定义一个命令字段,`public DelegateCommand<string> OpenCommand { get; private set; }` 这个命令是给其他控件来绑定的,出发这个命令,执行相应的方法

### 模块化

> 有多种引用动态库的方式，至于Prism会自动找到动态库里面的控件（前提是里面有）

- 同一个动态库内的类直接注入就行 
  - 在App.xaml.cs中`RegisterTypes`这个方法中注册
  - `containerRegistry.RegisterForNavigation<View1>();` 注册为导航
- 项目间引用
  - 引用项目后在App.xaml.cs中重写`ConfigureModuleCatalog`方法, `moduleCatalog.AddModule<ModuleAProfile>();` 添加动态库,Prism会自动识别库里面的ViewA
- 项目外引用
  - 在App.xaml.cs中重写`CreateModuleCatalog`方法
  - ` return new DirectoryModuleCatalog() { ModulePath = @".\Modules" }; ` Prism会自动加载里面的所有动态库,虽然这种方式基本解耦了,但是对于开发不利,不能加断点.

### 导航

> - **约定** 一个视图(View)Prism在实例化它的时候，会自动寻找这个ViewModels这个命名控件下，和View对应的ViewViewModel，这个是Prism里面固定的
> - **配置**  也可以手动绑定，在这个Profile（继承了IModule）的类的`RegisterTypes`方法中指定对应关系，`containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();` (这个优先级高一点，配置大于约定)

- 导航参数
  - 
