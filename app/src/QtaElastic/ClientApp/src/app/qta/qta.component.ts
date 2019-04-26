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
  private values = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  getSuggestions() {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let params = new HttpParams().set("query", this.values);
    this.http.get<QtaData[]>(this.baseUrl + 'api/Qta/QtaDatas', { headers: headers, params: params }).subscribe(result => {
      this.suggestions = result;
    }, error => console.error(error));
  }

  onChange(event: any) { // without type info
    this.values = event.target.value;
  }
}


interface QtaData {
  caption: string;
  text: string;
  page: string;
  weight: string;
  globalid: string;
}
