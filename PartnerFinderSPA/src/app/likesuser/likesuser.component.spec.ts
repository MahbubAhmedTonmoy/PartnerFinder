/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LikesuserComponent } from './likesuser.component';

describe('LikesuserComponent', () => {
  let component: LikesuserComponent;
  let fixture: ComponentFixture<LikesuserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LikesuserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LikesuserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
