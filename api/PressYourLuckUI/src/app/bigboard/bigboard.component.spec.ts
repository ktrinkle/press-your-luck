import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BigboardComponent } from './bigboard.component';

describe('BigboardComponent', () => {
  let component: BigboardComponent;
  let fixture: ComponentFixture<BigboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BigboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BigboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
