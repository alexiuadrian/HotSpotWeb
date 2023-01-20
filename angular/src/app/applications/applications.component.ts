import { Component, Injector } from "@angular/core";
import { finalize } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import {
  ApplicationDto,
  ApplicationServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { CreateApplicationDialogComponent } from "./create-application/create-application-dialog.component";

@Component({
  templateUrl: "./applications.component.html",
  animations: [appModuleAnimation()],
})
export class ApplicationsComponent extends PagedListingComponentBase<ApplicationDto> {
  applications: ApplicationDto[] = [];
  keyword: string = "";

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _applicationsService: ApplicationServiceProxy
  ) {
    super(injector);
  }

  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this._applicationsService
      .getList(true, true, undefined, undefined, undefined)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ApplicationDto[]) => {
        this.applications = result;
      });
  }

  protected delete(entity: ApplicationDto): void {
    throw new Error("Method not implemented.");
  }

  createApplication(): void {
    this.showCreateOrEditApplicationDialog();
  }

  showCreateOrEditApplicationDialog(id?: number): void {
    let createOrEditApplicationDialog: BsModalRef;
    if (!id) {
      createOrEditApplicationDialog = this._modalService.show(
        CreateApplicationDialogComponent,
        {
          class: "modal-lg",
        }
      );
    }
    // else {
    //   createOrEditApplicationDialog = this._modalService.show(
    //     EditRoleDialogComponent,
    //     {
    //       class: 'modal-lg',
    //       initialState: {
    //         id: id,
    //       },
    //     }
    //   );
  }
}
