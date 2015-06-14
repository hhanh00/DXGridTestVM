namespace ViewModel

open DevExpress.Xpf.Mvvm
open System.Collections.ObjectModel

type Row(n: int) = 
    inherit BindableBase()

    member x.N with get() = n
    member x.SqrN with get() = n*n
    member x.IsEven with get() = n%2 = 0

type CustomSummaryArgs =
    | Start
    | Calculate of int
    | Finish of System.Action<int>

type Data() = 
    inherit ViewModelBase()

    let mutable s = 0
    let listOfNumbers = new ObservableCollection<Row>()
    let customSummary (e: CustomSummaryArgs) = 
        printfn "%A" e
        match e with
        | Start -> s <- 0
        | Calculate n -> s <- s+n
        | Finish f -> f.Invoke(s)
        | _ -> ignore()

    do
        for i in 1..10 do
            listOfNumbers.Add(new Row(i))

    member x.Numbers with get() = listOfNumbers
    member x.CustomSummary with get() = new DelegateCommand<CustomSummaryArgs>(fun e -> customSummary e)

