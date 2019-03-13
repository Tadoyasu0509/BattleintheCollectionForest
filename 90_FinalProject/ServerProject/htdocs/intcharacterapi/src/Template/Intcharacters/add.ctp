<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\Intcharacter $intcharacter
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('List Intcharacters'), ['action' => 'index']) ?></li>
    </ul>
</nav>
<div class="intcharacters form large-9 medium-8 columns content">
    <?= $this->Form->create($intcharacter) ?>
    <fieldset>
        <legend><?= __('Add Intcharacter') ?></legend>
        <?php
            echo $this->Form->control('Name');
            echo $this->Form->control('Speed');
            echo $this->Form->control('Jump');
            echo $this->Form->control('Hp');
            echo $this->Form->control('Power');
            echo $this->Form->control('Item');
        ?>
    </fieldset>
    <?= $this->Form->button(__('Submit')) ?>
    <?= $this->Form->end() ?>
</div>
