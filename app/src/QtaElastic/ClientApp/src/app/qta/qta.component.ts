import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './qta.component.html',
  styleUrls: ['./qta.component.less']
})
export class QtaComponent {
  public suggestions: QtaData[];
  public docs: string[];
  private values = null;
  public selectedDoc = 'Select globalid...';
  public isDocIdSelected = false;
  public isActionButtonDisabled = true;
  public dropdownIsOpen = false;


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let params = new HttpParams().set("query", this.values);
    this.http.get<string[]>(this.baseUrl + 'api/Qta/AvailableDocuments', { headers: headers, params: params }).subscribe(result => {
      this.docs = result;
    }, error => console.error(error));
  }

  ChangeGlobalId(newSortOrder: string) {
    this.selectedDoc = newSortOrder;
    this.isDocIdSelected = true;
    this.toggleActionButtonEnabled();
  }


  toggleActionButtonEnabled() {
    this.isActionButtonDisabled = this.values == null || this.values == '' || !this.isDocIdSelected;
  }

  getSuggestions() {
    event.preventDefault();
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let params = new HttpParams().set("query", this.values).append("docid", this.selectedDoc);
    this.http.get<QtaData[]>(this.baseUrl + 'api/Qta/QtaDatas', { headers: headers, params: params }).subscribe(result => {
      this.suggestions = result;
      this.dropdownIsOpen = true;
    }, error => console.error(error));
  }

  onChange(event: any) { // without type info
    this.values = event.target.value;
    this.toggleActionButtonEnabled();
    return false;
  }
}


interface QtaData {
  caption: string;
  text: string;
  page: string;
  weight: string;
  globalid: string;
}
