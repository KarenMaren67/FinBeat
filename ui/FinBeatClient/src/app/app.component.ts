import {Component} from '@angular/core';
import {SomethingPagedComponent} from './something-paged/something-paged.component';
import {SomethingRewriteComponent} from './something-rewrite/something-rewrite.component';

@Component({
  selector: 'app-root',
  imports: [SomethingPagedComponent, SomethingRewriteComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
}
