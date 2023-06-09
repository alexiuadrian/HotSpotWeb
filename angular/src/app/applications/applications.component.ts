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
import { CLIENT_RENEG_LIMIT } from "tls";
import { Moment } from "moment";

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
        console.log(this.applications);
      });
  }

  protected delete(application: ApplicationDto): void {
    abp.message.confirm(
      this.l("RoleDeleteWarningMessage", application.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._applicationsService
            .delete(application.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l("SuccessfullyDeleted"));
                this.refresh();
              })
            )
            .subscribe(() => {});
        }
      }
    );
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

  editApplication(application: ApplicationDto): void {
    console.log(application);
  }

  startApplication(applicationId: number): void {
    this._applicationsService
      .runApplication(applicationId)
      .pipe(
        finalize(() => {
          abp.notify.success(this.l("SuccessfullyStarted"));
          this.refresh();
        })
      )
      .subscribe(() => {});
  }

  formatDate(date: Moment): string {
    return date.format("DD-MM-YYYY HH:mm");
  }
}
