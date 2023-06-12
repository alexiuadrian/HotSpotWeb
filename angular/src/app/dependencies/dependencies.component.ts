import { Component, Injector } from "@angular/core";
import { finalize } from "rxjs/operators";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import {
  Dependency,
  DependencyServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { CreateDependencyDialogComponent } from "./create-dependency/create-dependency-dialog.component";

@Component({
  templateUrl: "./dependencies.component.html",
  animations: [appModuleAnimation()],
  providers: [DependencyServiceProxy],
})
export class DependenciesComponent extends PagedListingComponentBase<Dependency> {
  dependencies: Dependency[] = [];
  keyword: string = "";
  totalItems: number = 0;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _dependencyService: DependencyServiceProxy
  ) {
    super(injector);
  }

  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this._dependencyService
      .getList(undefined, undefined, 100)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: Dependency[]) => {
        this.dependencies = result;
        this.totalItems = result.length;
      });
  }

  protected delete(dependency: Dependency): void {
    abp.message.confirm(
      this.l("RoleDeleteWarningMessage", dependency.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._dependencyService
            .delete(dependency.id)
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

  createDependency(): void {
    this.showCreateOrEditDependencyDialog();
  }

  showCreateOrEditDependencyDialog(id?: number): void {
    let createOrEditDependencyDialog: BsModalRef;
    if (!id) {
      createOrEditDependencyDialog = this._modalService.show(
        CreateDependencyDialogComponent,
        {
          class: "modal-lg",
        }
      );

      createOrEditDependencyDialog.content.onSave.subscribe(() => {
        this.refresh();
      });
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

  editDependency(dependency: Dependency): void {
    console.log("editDependency");
  }
}
