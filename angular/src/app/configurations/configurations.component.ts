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
  ConfigurationDto,
  ConfigurationServiceProxy,
  CreateConfigurationDto,
} from "@shared/service-proxies/service-proxies";
import { CreateConfigurationDialogComponent } from "./create-configuration/create-configuration-dialog.component";

@Component({
  templateUrl: "./configurations.component.html",
  animations: [appModuleAnimation()],
})
export class ConfigurationsComponent extends PagedListingComponentBase<ConfigurationDto> {
  configurations: ConfigurationDto[] = [];
  keyword: string = "";

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _configurationsService: ConfigurationServiceProxy
  ) {
    super(injector);
  }

  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this._configurationsService
      .getList(undefined, undefined, 100)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ConfigurationDto[]) => {
        this.configurations = result;
      });
  }

  protected delete(configuration: ConfigurationDto): void {
    abp.message.confirm(
      this.l("RoleDeleteWarningMessage", configuration.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._configurationsService
            .delete(configuration.id)
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

  createConfiguration(): void {
    this.showCreateOrEditConfigurationDialog();
  }

  showCreateOrEditConfigurationDialog(id?: number): void {
    let createOrEditConfigurationDialog: BsModalRef;
    if (!id) {
      createOrEditConfigurationDialog = this._modalService.show(
        CreateConfigurationDialogComponent,
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

  editConfiguration(configuration: ConfigurationDto): void {
    console.log("editConfiguration");
  }
}
