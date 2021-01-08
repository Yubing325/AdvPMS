import { Component, OnInit } from '@angular/core';
import { SprintService } from 'src/app/_services/sprint.service';
import { WorkItem } from '../../_models/workItem';
import { WorkItemState } from "../../_models/WorkItemState";

@Component({
  selector: 'app-work-item-grid',
  templateUrl: './work-item-grid.component.html',
  styleUrls: ['./work-item-grid.component.scss']
})
export class WorkItemGridComponent implements OnInit {

  workItems: WorkItem[] = [];
  state = WorkItemState;

  exportColumns: any[];

  constructor(private sprintService: SprintService) { }

  ngOnInit(): void {
    this.sprintService.getAllWorkItems().subscribe
    (
      (results:WorkItem[]) => {
        this.workItems = results;
      },
      error => {
        console.error(error);
      }
    );
  }

  exportPdf() {
    import("jspdf").then(jsPDF => {
        import("jspdf-autotable").then(x => {
            const doc = new jsPDF.default();
            //generate pdf
            doc.save('products.pdf');
        })
    })
}

exportExcel() {
    // import("xlsx").then(xlsx => {
    //     const worksheet = xlsx.utils.json_to_sheet(this.workItems);
    //     const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
    //     const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
    //     this.saveAsExcelFile(excelBuffer, "workItems");
    // });
}

saveAsExcelFile(buffer: any, fileName: string): void {
    import("file-saver").then(FileSaver => {
        let EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
        let EXCEL_EXTENSION = '.xlsx';
        const data: Blob = new Blob([buffer], {
            type: EXCEL_TYPE
        });
        FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
    });
}

}
